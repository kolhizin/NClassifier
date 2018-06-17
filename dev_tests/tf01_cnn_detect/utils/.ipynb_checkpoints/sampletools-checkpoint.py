import numpy as np


##########################################################################
##########################################################################
############## Used definitions                                        ###
################## sample - tuple (x0, x1, ..., z0, z1)                ###
################## region - tuple (center, dir, ortho, size)           ###
##########################################################################
##########################################################################


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
    tmp2 = generate_sample_spacing_1d(src_size[-1], sample_size[-1], stride[-1])
    tmp1 = generate_sample_spacing_nd(src_size[:-1], sample_size[:-1], stride[:-1])
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
######## dictregion: dict straight from json (or any other sources)    ###
########               (any orientation)                               ###
######## region: same as region, but tuple of ndarrays                 ###
########               (any orientation)                               ###
######## sample: tuple of img-coordinates (only )                      ###
########               (only up-orientation)                           ###
##########################################################################
##########################################################################


def transform_dictregion_to_region(region_info):
    center = np.float64([region_info['center']['X'], region_info['center']['Y']])
    direction = np.float64([region_info['dir']['X'], region_info['dir']['Y']])
    size = np.float64([region_info['w'], region_info['h']])
    return (center, direction, np.array([-direction[1], direction[0]]), size)

def transform_region_to_dictregion(region):
    def transform_vec(v):
        if v is None:
            return {'IsEmpty': True}
        return {'IsEmpty': False, 'X': v[0], 'Y': v[1]}
    return {
        'center': transform_vec(region[0]), 'dir': transform_vec(region[1]),
        'w': region[3][0], 'h': region[3][1]
    }

def check_region_dir(region, reference_direction = np.float64([0, 1]),
                            dot_prod_threshold = 0.9):
    return np.dot(region[1], reference_direction) > dot_prod_threshold

def transform_region_to_sample(region):
    assert(check_npregion_dir(region)) #check that we deal only with vertical
    lo_coord = (region[0] - region[3] + 1) * 0.5
    hi_coord = (region[0] + region[3] + 1) * 0.5
    return np.float64([lo_coord[0], hi_coord[0], 1-hi_coord[1], 1-lo_coord[1]])

def transform_sample_to_region(sample):
    lo_coord = np.float64([sample[0], 1 - sample[3]]) * 2 - 1
    hi_coord = np.float64([sample[1], 1 - sample[2]]) * 2 - 1
    return (0.5 * (lo_coord + hi_coord), np.float64([0,1]), np.float64([1,0]), 0.5 * (hi_coord - lo_coord))

def scale_sample(sample, scale):
    """
    Scale sample by scale.
    :param sample: tuple-like of coordinates (x0, x1, ..., y0, y1)
    :param scale: either scalar or tuple-like of scales (half the number of coordinates in scale)
    """
    if type(scale) in (list, tuple, np.ndarray):
        return np.float64([sample[i]*scale[i >> 1] for i in range(len(sample))])
    return np.float64([sample[i]*scale for i in range(len(sample))])

##########################################################################
##########################################################################
#### Block for handling image and region rotation                      ###
##########################################################################
##########################################################################

def generate_rotation_information(img_size, alpha_rad):
    """
    Returns rotation information for warpAffine in OpenCV (rotation_matrix, translation_vector, new_img_size)
    :param img_size: old size of image
    :param alpga_rad: rotation angle in radians
    :return: (rotation_matrix, translation_vector, new_img_size)
    """
    pcenter = np.array(img_size) * 0.5
    
    cosa = np.cos(alpha_rad)
    sina = np.sin(alpha_rad)
    
    new_w = np.ceil(img_size[0] * np.abs(cosa) + img_size[1] * np.abs(sina))
    new_h = np.ceil(img_size[0] * np.abs(sina) + img_size[1] * np.abs(cosa))
    
    rot_M = np.array([[cosa, sina], [-sina, cosa]])
    off_V = np.array([new_w, new_h])*0.5 - np.matmul(rot_M, pcenter)
    return (rot_M, off_V.reshape(-1,1), np.array([new_w, new_h]))

def adjust_region_for_image_rotation(region, rot_matrix, old_size, new_size):
    """
    Adjust region for rotation of image.
    :param region: region to be adjusted.
    :param rot_matrix: rotation matrix used for image rotation.
    :param old_size: old size of image
    :param new_size: new size of image
    :return: adjusted region
    """
    np_old_size = np.array(old_size)
    np_new_size = np.array(new_size)
    
    
    res_center = np.matmul(rot_matrix.transpose(), region[0] * np_old_size) / np_new_size    
    res_dir = np.matmul(rot_matrix.transpose(), region[1] * np_old_size) / np_new_size
    res_ortho = np.matmul(rot_matrix.transpose(), region[2] * np_old_size) / np_new_size
        
    tmp_dir_len = np.sqrt(np.dot(res_dir, res_dir))
    tmp_ortho_len = np.sqrt(np.dot(res_ortho, res_ortho))
    res_dir = res_dir / tmp_dir_len    
    res_ortho = res_ortho / tmp_ortho_len        
        
    return res_center, res_dir, res_ortho, region[3]*np.array([tmp_ortho_len, tmp_dir_len])

##########################################################################
##########################################################################
#### Block for generation of target for samples,                       ###
####       main function - calculate_sample_chain_target               ###
##########################################################################
######## What we have: image of specified size, specified sample sizes,###
########               specified target regions                        ###
######## Parameters: relevancy-function & cutoff, target-function      ###
##########################################################################
##########################################################################

def intersect_poly_by_axis(points, axis_id, axis_offset, axis_dir):
    """
    Cuts polygon by axis (hyperplane) parallel to axis.
    :param points: polygon points in order
    :param axis_id: index of hyperplane parallel to axis
    :param axis_offset: offset of hyperplane
    :param axis_dir: +1 or -1 to specify direction
    :return: list of points (may be empty)
    """
    def intersect_point(pnt1, pnt2):
        a = (pnt2[axis_id] - axis_offset) / (pnt2[axis_id] - pnt1[axis_id])
        return pnt1 * a + pnt2 * (1 - a)
        
    def deduct_point(ind1, pres1, ind2, pres2):
        if (pres1 and pres2):
            return [points[ind1]]
        if pres1:
            return [points[ind1], intersect_point(points[ind1], points[ind2])]
        if pres2:
            return [intersect_point(points[ind1], points[ind2])]
        return []
        
    if len(points) == 0:
        return []
    location = [(p[axis_id] - axis_offset) * axis_dir >= 0 for p in points]
    points_info = list(zip(location, range(len(location))))
    points_info.append(points_info[0])
    return [z for ((p1, i1), (p2, i2)) in zip(points_info[:-1], points_info[1:]) for z in deduct_point(i1, p1, i2, p2)]

def intersect_poly_aabb(points, aabb):
    """
    Intersects polygon and AABB.
    :param points: polygon points in order
    :param aabb: definition of aabb (x0, x1, y0, y1, ...)
    :return: list of points (may be empty)
    """
    res = points
    axis_dir = 1
    for i in range(len(aabb)):
        if len(res) == 0:
            return []
        res = intersect_poly_by_axis(res, i >> 1, aabb[i], axis_dir)
        axis_dir *= -1
    return res

def calculate_poly_area_2d(points):
    """
    Calculates area of polygon.
    :param points: polygon points in order
    :return: area
    """
    if len(points) == 0:
        return 0
    extended_points = points + [points[0]]
    return 0.5*np.sum([(p1[0]+p2[0])*(p1[1]-p2[1]) for (p1, p2) in zip(extended_points[:-1], extended_points[1:])])

def generate_region_points(region):
    """
    Generates corner points for region.
    :param region: (center, dir, ortho, size) - region definition
    :return: list of corner-points
    """
    coords = [np.array([-region[3][0], region[3][1]]), np.array([region[3][0], region[3][1]]),
              np.array([region[3][0], -region[3][1]]), np.array([-region[3][0], -region[3][1]])]
    points = [region[0] + p[0] * region[2] + p[1] * region[1] for p in coords]
    return [np.array([0.5+0.5*p[0], 0.5-0.5*p[1]]) for p in points]

def generate_bounding_sample(points):
    """
    Generates bounding sample for list of points.
    :param points: list of points
    :return: tuple of (x0, x1, y0, y1, ...)
    """
    full = np.array(points)
    minimax = [np.min(full, axis=0), np.max(full, axis=0)]
    return tuple(minimax[i % 2][i >> 1] for i in range(len(points[0])*2))

def calculate_sample_overlap_volume(sample1, sample2):
    """
    Calculates intersection volume of sample1 and sample2.
    Arguments must be of same scale (coord system, etc.)
    :param sample1: tuple-like of coordinates (x0, x1, ..., y0, y1)
    :param sample2: tuple-like of coordinates (x0, x1, ..., y0, y1)
    :return: area of intersection (or zero if none)
    """
    x_areas = [max(0.0, min(sample1[i+1], sample2[i+1]) - max(sample1[i], sample2[i]))
               for i in range(0, len(sample1), 2)]    
    return np.prod(x_areas)

def calculate_samples_relevant_regions_target(samples, regions, img_size, relevancy, relevancy_cutoff, target):
    """
    For each sample find most relevant region using relevancy-function and calculate target if
    relevancy passes cutoff.
    Relevancy function is calculated based on 3 numbers: sample-area, region-area, overlap-area.
    Target function is calculated based on sample and region.
    :param samples: list of tuple-like of coordinates (x0, x1, ..., y0, y1)
    :param regions: list of tuple-like of coordinates (x0, x1, ..., y0, y1)
    :param relevancy: (scalar, scalar, scalar) => scalar (not nan) - relevency function of arguments
                sample-area, region-area, overlap-area;
                default: most-relevant region is with highest absolute overlap
    :param relevancy_cutoff: cutoff point for relevancy - if does not passes, then None is propagated
    :param target: (sample, region, img_size, sample_area, region_area, overlap_area) => scalar - target function
    :return: list of targets
    """
    num_regions = len(regions)
    regions_poly = [[z * np.array(img_size) for z in generate_region_points(r)] for r in regions]
    regions_aabb = [generate_bounding_sample(r) for r in regions_poly]    
    regions_area = [calculate_poly_area_2d(r) for r in regions_poly]
    
    def calc_approximate_relevant(sample):
        return [i for (i,x) in enumerate(regions_aabb) if calculate_sample_overlap_volume(sample, x)>0.0]
    
    def calc_relevant_target(sample):
        sample_area = np.prod([sample[i+1]-sample[i] for i in range(0, len(sample), 2)])
        approx_relevant = calc_approximate_relevant(sample)
        if len(approx_relevant) == 0:
            return target(sample, None, img_size, sample_area, None, None)
        
        
        overlaps = [calculate_poly_area_2d(intersect_poly_aabb(regions_poly[i], sample)) for i in approx_relevant]
        relevancy_res =  [relevancy(sample_area, regions_area[i], overlaps[ind]) for (ind, i) in enumerate(approx_relevant)]
        most_relevant_id = np.argmax(relevancy_res)
        if relevancy_res[most_relevant_id] <= relevancy_cutoff:
            return target(sample, None, img_size, sample_area, None, None)
        
        res_id = approx_relevant[most_relevant_id]
        return target(sample, regions[res_id], img_size, sample_area, regions_area[res_id], overlaps[most_relevant_id])
    
    if num_regions == 0:
        return [target(sample, None, img_size,
                      np.prod([sample[i+1]-sample[i] for i in range(0, len(sample), 2)]),
                      None, None) for sample in samples]
    
    return [calc_relevant_target(sample) for sample in samples]

def calculate_sample_target_pct_sample_area(sample, region, img_size, sample_area, region_area, overlap_area):
    if region is None:
        return 0.0
    return overlap_area / sample_area

def calculate_sample_target_pct_region_area(sample, region, img_size, sample_area, region_area, overlap_area):
    if region is None:
        return 0.0
    return overlap_area / region_area

def calculate_sample_chain_target(sample_chain, regions,
            relevancy=lambda a,b,c:c, relevancy_cutoff=1e-7,
            target=calculate_sample_target_pct_sample_area):
    """
    Calculate targets for sample-chain.
    :param sample_chain: list of tuple of (img_size, list of samples),
                         i.e. like in generate_sample_chain
    :param regions: list of tuple-like of regions (x0, x1, ..., y0, y1)
    :return: list of list of hits (almost same structure as sample_chain)
    """
    return [
        calculate_samples_relevant_regions_target(samples, regions, img_size,
                        relevancy, relevancy_cutoff, target)
        for (img_size, samples) in sample_chain
    ]

def transform_sample_chain_to_linear_form(sample_chain, targets = None):
    """
    Transforms sample_chain (and targets, if specified) to linear form, returning list of tuples (image-level-id, x0, x1, y0, y1, [target])
    and image size chain: list of tuples (width, height).
    :param sample_chain: list of pair (img-size, list of samples);
    :param targets: either None or corresponding structure with targets
    :return: list of tuples (image-level-id, x0, x1, y0, y1, [target]) and list of image sizes
    """
    num_levels = len(sample_chain)
    res_samples = [
        (i, *val) if len(val) > 2 else (i, *val[0], val[1])
        for i in range(num_levels)
        for val in (zip(sample_chain[i][1], targets[i]) if targets is not None else sample_chain[i][1])
    ]
    res_image_chain = [v for (v,_) in sample_chain]
    return res_samples, res_image_chain


##########################################################################
##########################################################################
#### Block for generation of mipmaps                                   ###
####       main function - generate_image_miplevels                    ###
##########################################################################
######## What we have: source image and specified mip-level sizes      ###
##########################################################################
######## Parameters: minimal downscale factor                          ###
##########################################################################
##########################################################################


def generate_optimal_resize_sources(size_chain, minimal_factor):
    """
    Generates indices of sizes from where to downsize image. It assumes that size_chain is decreasing list of image-sizes.
    :param size_chain: list of tuples (width, height)
    :param minimal_factor: factor for downsize, i.e. use smallest source where source-size > minimal-factor * cur-size
    :return: list of indices, e.g. [0,0,1,2,2,3]
    """
    def calc_minimal_factor(size0, size1):
        if type(size0) in (tuple, list, np.ndarray):
            return np.min([a / b for (a,b) in zip(size0, size1)])
        return size0 / size1
    
    res = [0 for x in size_chain] #initialize with first element
    for i in range(1, len(size_chain)-1):
        for j in range(i + 1, len(size_chain)):
            if calc_minimal_factor(size_chain[i], size_chain[j]) >= minimal_factor:
                res[j] = i
    return res

def generate_image_miplevels(src_image, size_chain, resize_function, minimal_factor=1.9):
    """
    Generates miplevels from src_image according to size_chain.
    :param src_image: source image
    :param size_chain: list of (width, height) - it must be decreasing
    :param resize_function: function (image, new_size) => image to resize image
    :param minimal_factor: factor for downsize, i.e. use smallest source where source-size > minimal-factor * cur-size
    :return: list of images of corresponding size
    """
    src_size = (src_image.shape[1], src_image.shape[0])
    extended_chain = [src_size] + size_chain
    extended_source = generate_optimal_resize_sources(extended_chain, minimal_factor)
    res_images = [src_image]
    for i in range(1, len(extended_chain)):
        tmp_size = extended_chain[extended_source[i]]
        res_images.append(resize_function(res_images[extended_source[i]], extended_chain[i]))
    return res_images[1:]

##########################################################################
##########################################################################
#### Block for extraction of features                                  ###
####       main function - generate_image_miplevels                    ###
##########################################################################
##########################################################################
##########################################################################

def extract_features_from_samples_and_mipmaps(samples, mipmaps):
    """
    Extracts features from images according to sample description. Does not perform any transformation of channels.
    :param sample: list of (mip-id, x0, x1, y0, y1)
    :param mipmaps: list of images
    :return: array of num_samples x sample_rows x sample_cols x channels
    """
    return np.array([
        mipmaps[x[0]][x[3]:x[4],x[1]:x[2]] for x in samples
    ])

def extract_targets_from_samples(samples):
    """
    Extracts targets from sample description.
    :param sample: list of (mip-id, x0, x1, y0, y1, target)
    :return: array of num_samples of targets
    """
    return np.array([x[5] for x in samples])

def split_sample(sample, pcts):
    """
    Splits sample into number of parts according to length of pcts.
    :param sample: tuple of samples or sample itself
    :param pcts: shares to which sample should be split
    :return: tuple of sample-like structures
    """
    cumvpct = np.array(pcts)
    cumvpct = cumvpct / np.sum(cumvpct)
    cumvpct = np.append(-0.1, np.cumsum(cumvpct))
    cumvpct[-1] = 1.1 #in order to exclude (1 < 1) situations
    ranges = [(cumvpct[i], cumvpct[i+1]) for i in range(len(pcts))]
    if type(sample) is tuple:
        z = np.random.uniform(size=len(sample[0]))
        if type(sample[0]) is list:
            w = np.array(range(len(sample[0])))
            return tuple(tuple([x[y] for y in w[(z >= a) & (z < b)]] for x in sample) for (a,b) in ranges)
        else:            
            return tuple(tuple(x[(z >= a) & (z < b)] for x in sample) for (a,b) in ranges)
    else:
        z = np.random.uniform(size=len(sample))
        if type(sample) is list:
            w = np.array(range(len(sample)))
            return tuple([sample[y] for y in w[(z >= a) & (z < b)]] for (a,b) in ranges)
        else:
            return tuple(sample[(z >= a) & (z < b)] for (a,b) in ranges)
        
def shuffle_batches(sample, batch_size, shuffle=True):
    """
    Shuffles sample into parts of batch_size, yielding them one by one
    :param sample: tuple of samples or sample itself
    :param batch_size: batch size
    :return: yields sample-like structures
    """
    if type(sample) is tuple: 
        ids = list(range(len(sample[0])))
        if shuffle:
            np.random.shuffle(ids)
        for i in range(0,len(ids),batch_size):
            lst = min(len(ids), i + batch_size)
            if type(sample[0]) is list:
                yield ([x[z] for z in ids[i:lst]] for x in sample)
            else:
                yield (np.array(x[ids[i:lst],]) for x in sample)
    else:
        ids = list(range(len(sample)))
        if shuffle:
            np.random.shuffle(ids)
        for i in range(0,len(ids),batch_size):
            lst = min(len(ids), i + batch_size)
            if type(sample) is list:
                yield [sample[z] for z in ids[i:lst]]
            else:
                yield np.array(sample[ids[i:lst],])