import numpy as np

##########################################################################
##########################################################################
#### Block for generation of samples, main function -                  ###
####                                    generate_sample_chain          ###
##########################################################################
######## What we have: image of specified size, specified sample sizes ###
######## Parameters: image size step (e.g. powers of two) and strides  ###
##########################################################################
######## Returns: all possible samples (coordinates) and used sizes    ###
##########################################################################
##########################################################################

def generate_sample_spacing_1d(src_size, sample_size, stride):
    num = np.ceil((src_size - sample_size) / stride)
    spc = np.linspace(0, src_size - sample_size, num + 1)
    tmp = [np.round(x).astype(np.int32) for x in spc]
    return [(x, x + sample_size) for x in tmp]

def generate_sample_spacing_nd(src_size, sample_size, stride):
    if type(src_size) not in (list, tuple, np.ndarray):
        return generate_sample_spacing_1d(src_size, sample_size, stride)
    if len(src_size) == 1:
        return generate_sample_spacing_1d(src_size[0], sample_size[0], stride[0])
    tmp1 = generate_sample_spacing_1d(src_size[-1], sample_size[-1], stride[-1])
    tmp2 = generate_sample_spacing_nd(src_size[:-1], sample_size[:-1], stride[:-1])
    return [(*x, *y) for x in tmp1 for y in tmp2]

def generate_size_chain_1d(src_size, minimal_size, factor):
    log_src = np.log10(src_size)
    log_min = np.log10(minimal_size)
    log_factor = np.log10(factor)
    num_points = np.abs((log_src - log_min) / log_factor)
    num_points = np.ceil(np.round(num_points, decimals=7)).astype(int)
    res = np.logspace(log_src, log_min, num_points + 1)
    return [np.round(x).astype(int) for x in res]

def generate_sample_chain(img_size, minimal_size, step_factor, sample_size, sample_stride):
    """
    Returns list of samples with corresponding link to image.
    Iterates through different sizes: from minimal_size to img_size approximately by step_factor;
    Iterates through different offsets for each size with approximate stride of sample_stride;
    :param img_size: original size of image (tuple of integers or single integer)
    :param minimal_size: minimal size, to which image should be shrunk
    :param step_factor: approximate step for image shrinkage
    :param sample_size: final size of sample
    :param sample_stride: stride to be used for samples
    :return: list of (size, list of samples); sample is tuple of form (x0, x1, y0, y1, .., z0, z1),
        where img[x0:x1,y0:y1,..,z0:z1] is resulting sample
    """
    if type(img_size) in (tuple, list, np.ndarray):
        zipped_size_params = zip(img_size, minimal_size, step_factor)
        img_sizes = list(zip(*[generate_size_chain_1d(*x) for x in zipped_size_params]))
    else:
        img_sizes = generate_size_chain_1d(img_size, minimal_size, step_factor)
    
    return [(x, generate_sample_spacing_nd(x, sample_size, sample_stride)) for x in img_sizes]

##########################################################################
##########################################################################
#### Block for handling region-description                             ###
##########################################################################
######## region: dict straight from json (or any other sources)        ###
########               (any orientation)                               ###
######## npregion: same as region, but tuple of ndarrays               ###
########               (any orientation)                               ###
######## imgregion: tuple of img-coordinates (only )                   ###
########               (only up-orientation)                           ###
##########################################################################
##########################################################################


def transform_region_to_npregion(region_info):
    center = np.float64([region_info['center']['X'], region_info['center']['Y']])
    direction = np.float64([region_info['dir']['X'], region_info['dir']['Y']])
    size = np.float64([region_info['w'], region_info['h']])
    return (center, direction, size)

def transform_npregion_to_region(npregion):
    def transform_vec(v):
        if v is None:
            return {'IsEmpty': True}
        return {'IsEmpty': False, 'X': v[0], 'Y': v[1]}
    return {
        'center': transform_vec(npregion[0]), 'dir': transform_vec(npregion[1]),
        'w': npregion[2][0], 'h': npregion[2][1]
    }

def check_npregion_dir(npregion, reference_direction = np.float64([0, 1]),
                            dot_prod_threshold = 0.9):
    return np.dot(npregion[1], reference_direction) > dot_prod_threshold

def transform_npregion_to_imgregion(npregion):
    assert(check_npregion_dir(npregion)) #check that we deal only with vertical
    lo_coord = (npregion[0] - npregion[2] + 1) * 0.5
    hi_coord = (npregion[0] + npregion[2] + 1) * 0.5
    return np.float64([lo_coord[0], hi_coord[0], 1-hi_coord[1], 1-lo_coord[1]])

def transform_imgregion_to_npregion(imgregion):
    lo_coord = np.float64([imgregion[0], 1 - imgregion[3]]) * 2 - 1
    hi_coord = np.float64([imgregion[1], 1 - imgregion[2]]) * 2 - 1
    return (0.5 * (lo_coord + hi_coord), np.float64([0,1]), 0.5 * (hi_coord - lo_coord))

def scale_imgregion(imgregion, scale):
    """
    Scale imgregion by scale.
    :param imgregion: tuple-like of coordinates (x0, x1, ..., y0, y1)
    :param scale: either scalar or tuple-like of scales (half the number of coordinates in imgregion)
    """
    if type(scale) in (list, tuple, np.ndarray):
        return np.float64([imgregion[i]*scale[i >> 1] for i in range(len(imgregion))])
    return np.float64([imgregion[i]*scale for i in range(len(imgregion))])

##########################################################################
##########################################################################
#### Block for handling image and region rotation                      ###
##########################################################################
######## region: dict straight from json (or any other sources)        ###
########               (any orientation)                               ###
######## npregion: same as region, but tuple of ndarrays               ###
########               (any orientation)                               ###
######## imgregion: tuple of img-coordinates (only )                   ###
########               (only up-orientation)                           ###
##########################################################################
##########################################################################

def generate_rotation_information(img_size, alpha_rad):
    """
    returns (rotation_matrix, translation_vector, new_img_size)
    """
    pcenter = np.array(img_size) * 0.5
    
    cosa = np.cos(alpha_rad)
    sina = np.sin(alpha_rad)
    
    new_w = np.ceil(img_size[0] * np.abs(cosa) + img_size[1] * np.abs(sina))
    new_h = np.ceil(img_size[0] * np.abs(sina) + img_size[1] * np.abs(cosa))
    
    rot_M = np.array([[cosa, sina], [-sina, cosa]])
    off_V = np.array([new_w, new_h])*0.5 - np.matmul(rot_M, pcenter)
    return (rot_M, off_V.reshape(-1,1), np.array([new_w, new_h]))

##########################################################################
##########################################################################
#### Block for generation of target for samples,                       ###
####       main function - calculate_sample_chain_target               ###
##########################################################################
######## What we have: image of specified size, specified sample sizes,###
########               specified target imgregions                     ###
######## Parameters: relevancy-function & cutoff, target-function      ###
##########################################################################
##########################################################################

def calculate_sample_overlap_area_1d(sample1, sample2):
    """
    Calculates intersection area of sample1 and sample2.
    Arguments must be of same scale (coord system, etc.)
    :param sample1: tuple-like of coordinates (x0, x1)
    :param sample2: tuple-like of coordinates (x0, x1)
    :return: area (length) of intersection (or zero if none)
    """
    return max(0.0, min(sample1[1], sample2[1]) - max(sample1[0], sample2[0]))

def calculate_sample_overlap_area_nd(sample1, sample2):
    """
    Calculates intersection area of sample1 and sample2.
    Arguments must be of same scale (coord system, etc.)
    :param sample1: tuple-like of coordinates (x0, x1, ..., y0, y1)
    :param sample2: tuple-like of coordinates (x0, x1, ..., y0, y1)
    :return: area of intersection (or zero if none)
    """
    x_areas = [calculate_sample_overlap_area_1d(sample1[i:(i+2)], sample2[i:(i+2)])
               for i in range(0, len(sample1), 2)]
    
    return np.prod(x_areas)

def calculate_samples_relevant_imgregions_target(samples, imgregions,
        relevancy, relevancy_cutoff,
        target):
    """
    For each sample find most relevant img-region using relevancy-function and calculate target if
    relevancy passes cutoff.
    Relevancy function is calculated based on 3 numbers: sample-area, region-area, overlap-area.
    Target function is calculated based on sample and imgregion.
    :param samples: list of tuple-like of coordinates (x0, x1, ..., y0, y1)
    :param regions: list of tuple-like of coordinates (x0, x1, ..., y0, y1)
    :param relevancy: (scalar, scalar, scalar) => scalar (not nan) - relevency function of arguments
                sample-area, region-area, overlap-area;
                default: most-relevant region is with highest absolute overlap
    :param relevancy_cutoff: cutoff point for relevancy - if does not passes, then None is propagated
    :param target: (sample, imgregion) => scalar - target function
    :return: list of targets
    """
    num_regions = len(imgregions)
    imgregions_area = [np.prod([x[i+1]-x[i] for i in range(0, len(x), 2)]) for x in imgregions]
    
    def calc_relevant_target(sample):
        sample_area = np.prod([sample[i+1]-sample[i] for i in range(0, len(sample), 2)])
        overlaps = [calculate_sample_overlap_area_nd(sample, x) for x in imgregions]
        relevancy_res =  [relevancy(sample_area, imgregions_area[i], overlaps[i])
                          for i in range(len(overlaps))]
        most_relevant_id = np.argmax(relevancy_res)
        if relevancy_res[most_relevant_id] <= relevancy_cutoff:
            return target(sample, None)
        return target(sample, imgregions[most_relevant_id])
    
    return [calc_relevant_target(sample) for sample in samples]

def calculate_sample_target_pct_sample_area(sample, imgregion):
    if imgregion is None:
        return 0.0
    overlap = calculate_sample_overlap_area_nd(sample, imgregion)
    sample_area = np.prod([sample[i+1]-sample[i] for i in range(0, len(sample), 2)])
    return overlap / sample_area

def calculate_sample_target_pct_imgregion_area(sample, imgregion):
    if imgregion is None:
        return 0.0
    overlap = calculate_sample_overlap_area_nd(sample, imgregion)
    imgregion_area = np.prod([imgregion[i+1]-imgregion[i] for i in range(0, len(imgregion), 2)])
    return overlap / imgregion_area

def calculate_sample_chain_target(sample_chain, imgregions,
            relevancy=lambda a,b,c:c, relevancy_cutoff=1e-7,
            target=calculate_sample_target_pct_sample_area):
    """
    Calculate targets for sample-chain.
    :param sample_chain: list of tuple of (img_size, list of samples),
                         i.e. like in generate_sample_chain
    :param regions: list of tuple-like of imgregions (x0, x1, ..., y0, y1)
    :return: list of list of hits (almost same structure as sample_chain)
    """
    return [
        calculate_samples_relevant_imgregions_target(samples,
                        [scale_imgregion(r, img_size) for r in imgregions],
                        relevancy, relevancy_cutoff, target)
        for (img_size, samples) in sample_chain
    ]