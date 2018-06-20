import numpy as np
import cv2
import pickle
import os, os.path
import utils.sampletools as smpl

def generate_sample_with_target(image, regions, **kwargs):
    """
    Generate features and targets for image and specified regions.
    :param image: image to be used for feature engineering
    :param regions: target regions to be used for target engineering
    :param max_size: default=1024, integer or pair of integers specifying maximal size of image
                    to be used in sample generation. If image is greater it will be first shrunk
                    to be not larger than max_size.
    :param min_size: default=32, integer or pair of integers specifying minimal size of image
                    to be used in sample generation. Image will not be shrunk below this size (if
                    at least one axis is below this size, process stops).
    :param size_step: default=0.7071, float or pair of floats specifying multiplicative step
                    which will be used for shrinking of image.
    :param sample_size: default=16, integer or pair of integers specifying size of sample, which
                    will be used for both feature and target engineering.
    :param sample_stride: default=2, integer or pair of integers specifying stride with which
                    samples to be applied to image.
    :param relevancy: default=lambda a,b,c:c (most-relevant region is with highest absolute overlap),
                (scalar, scalar, scalar) => scalar (not nan) relevancy function of arguments
                sample-area, region-area, overlap-area;
    :param relevancy_cutoff: default=1e-7, cutoff point for region relevancy
    :param target: deafult=sampletools.calculate_sample_target_pct_sample_area - largest overlap/sample
            (sample, region, img_size, sample_area, region_area, overlap_area) => scalar
            target function that based on these parameters returns any kind of object
    """
    def expand2tuple(x):
        if type(x) in (tuple, list, np.ndarray):
            return x
        return (x, x)

    imgsize = (image.shape[1], image.shape[0])
    
    loc_max_size = expand2tuple(kwargs.get('max_size', 1024))
    loc_min_size = expand2tuple(kwargs.get('min_size', 32))
    loc_size_step = expand2tuple(kwargs.get('size_step', 0.7071))
    loc_sample_size = expand2tuple(kwargs.get('sample_size', 16))
    loc_sample_stride = expand2tuple(kwargs.get('sample_stride', 2))

    if imgsize[0] > loc_max_size[0] or imgsize[1] > loc_max_size[1]:
        size_factor = min(loc_max_size[0] / imgsize[0], loc_max_size[1] / imgsize[1])
        #deliberately floor in order not to exceed max_size
        imgsize = (int(np.floor(imgsize[0] * size_factor)), int(np.floor(imgsize[1] * size_factor)))

    samples = smpl.generate_sample_chain(imgsize, loc_min_size, loc_size_step, loc_sample_size, loc_sample_stride)
    targets = smpl.calculate_sample_chain_target(samples, regions,
                    relevancy=kwargs.get('relevancy', lambda a,b,c:c),
                    relevancy_cutoff=kwargs.get('relevancy_cutoff', 1e-7),
                    target=kwargs.get('target', smpl.calculate_sample_target_pct_sample_area))
    fullsample, size_chain = smpl.transform_sample_chain_to_linear_form(samples, targets)
    return fullsample, size_chain


def generate_sample_with_target_from_obs(observation, **kwargs):
    """
    Generate features and targets for observation.
    :param observation: dict (json-like structure) containing filename of image and description of
                        regions.
    :param path: default=''. Path where to search for image.
    :param max_size: default=1024, integer or pair of integers specifying maximal size of image
                    to be used in sample generation. If image is greater it will be first shrunk
                    to be not larger than max_size.
    :param min_size: default=32, integer or pair of integers specifying minimal size of image
                    to be used in sample generation. Image will not be shrunk below this size (if
                    at least one axis is below this size, process stops).
    :param size_step: default=0.7071, float or pair of floats specifying multiplicative step
                    which will be used for shrinking of image.
    :param sample_size: default=16, integer or pair of integers specifying size of sample, which
                    will be used for both feature and target engineering.
    :param sample_stride: default=2, integer or pair of integers specifying stride with which
                    samples to be applied to image.
    :param relevancy: default=lambda a,b,c:c (most-relevant region is with highest absolute overlap),
                (scalar, scalar, scalar) => scalar (not nan) relevancy function of arguments
                sample-area, region-area, overlap-area;
    :param relevancy_cutoff: default=1e-7, cutoff point for region relevancy
    :param target: deafult=sampletools.calculate_sample_target_pct_sample_area - largest overlap/sample
            (sample, region, img_size, sample_area, region_area, overlap_area) => scalar
            target function that based on these parameters returns any kind of object
    """
    regions = [smpl.transform_dictregion_to_region(x) for x in observation['regions']]

    fname = os.path.join(kwargs.get('path', ''), observation['imgName'])
    image = cv2.imread(fname)
    
    fullsample, size_chain = generate_sample_with_target(image, regions,
                                **{k:v for (k,v) in kwargs.items() if k not in ('path')})
    return fullsample, size_chain, fname

def process_samples_with_targets(fname, **kwargs):
    """
    Process region-description files, create corresponding pickle files with processed samples and targets.
    :param observation: dict (json-like structure) containing filename of image and description of
                        regions.
    :param path: default=''. Path where to search for image.
    :param max_size: default=1024, integer or pair of integers specifying maximal size of image
                    to be used in sample generation. If image is greater it will be first shrunk
                    to be not larger than max_size.
    :param min_size: default=32, integer or pair of integers specifying minimal size of image
                    to be used in sample generation. Image will not be shrunk below this size (if
                    at least one axis is below this size, process stops).
    :param size_step: default=0.7071, float or pair of floats specifying multiplicative step
                    which will be used for shrinking of image.
    :param sample_size: default=16, integer or pair of integers specifying size of sample, which
                    will be used for both feature and target engineering.
    :param sample_stride: default=2, integer or pair of integers specifying stride with which
                    samples to be applied to image.
    :param relevancy: default=lambda a,b,c:c (most-relevant region is with highest absolute overlap),
                (scalar, scalar, scalar) => scalar (not nan) relevancy function of arguments
                sample-area, region-area, overlap-area;
    :param relevancy_cutoff: default=1e-7, cutoff point for region relevancy
    :param target: deafult=sampletools.calculate_sample_target_pct_sample_area - largest overlap/sample
            (sample, region, img_size, sample_area, region_area, overlap_area) => scalar
            target function that based on these parameters returns any kind of object
    """
    src_path = kwargs.get('path', os.path.split(fname)[0])
    src_file = os.path.split(fname)[1]

    with open(os.path.join(src_path, src_file), 'r') as f:
        src_sample = json.load(f)

    desc_name = src_file.split('.')[0]
    for v in src_sample.values():
        sample, size_chain, fname = trnt.generate_sample_with_target_from_obs(v, path=src_path,
                                            **{k:x for (k,x) in kwargs.items() if k not in ('path')})
        img_name = os.path.split(fname)[1].split('.')[0]
        sname = os.path.join(src_path,  '{0}_{1}.pickle'.format(img_name, desc_name))
        with open(sname, 'wb') as f:
            pickle.dump((sample, size_chain, fname), f, protocol=4)

def generate_batches_from_image_sample(image, sample, size_chain, batch_size,
                                       shuffle=True, supress_target=False):
    """
    Uses loaded image and sample information to generate batches of features and target, which it yields.
    :param image: image to be used fo batches
    :param sample: list of (mip-id, x0, x1, y0, y1, [target - optional])
    :param size_chain: if list of image-sizes to be used for transformation of image -- must be concordant
                with sample
    :param batch_size: number of observations in single batch (less or equal to specified number)
    :param shuffle: if True, will randomly shuffle observations, if False won't mess with the order.
    :param supress_target: if True will not yield targets
    """
    yield_target = len(sample[0])>5
    if supress_target:
        yield_target = False
        
    mipimgs = smpl.generate_image_miplevels(image, size_chain, resize_function=lambda x, s:cv2.resize(x, dsize=(s[0], s[1])))
    for s in smpl.shuffle_batches(sample, batch_size=batch_size, shuffle=shuffle):
        features = smpl.extract_features_from_samples_and_mipmaps(s, mipimgs)
        if yield_target:
            targets = smpl.extract_targets_from_samples(s)
            yield (features, targets)
        else:
            yield features
        
def generate_batches_from_file(fname, batch_size, shuffle=True, supress_target=False):
    """
    Loads image and sample information to generate batches of features and target, which it yields.
    :param fname: name of sample-description
    :param batch_size: number of observations in single batch (less or equal to specified number)
    :param shuffle: if True, will randomly shuffle observations, if False won't mess with the order.
    :param supress_target: if True will not yield targets
    """
    with open(fname, 'rb') as f:
        sample, size_chain, img_fname = pickle.load(f)
    image = cv2.cvtColor(cv2.imread(img_fname), cv2.COLOR_BGR2RGB)
    for s in generate_batches_from_image_sample(image, sample, size_chain, batch_size,
                                                shuffle=shuffle, supress_target=supress_target):
        yield s
        
        

def combine_value_map_2d_1level(samples, values):
    """
    Combines 2d value map for 1 mip-level, using sample-values.
    :param samples: sample description
    :param values: corresponding values
    :returns: 2d value map
    """
    num_dim = len(samples[0]) >> 1
    mins = np.array([np.min([s[i * 2] for s in samples]) for i in range(num_dim)])
    maxs = np.array([np.max([s[i * 2 + 1] for s in samples]) for i in range(num_dim)])
    dims = maxs - mins
    
    sums = np.zeros(dims)
    cnts = np.zeros(dims)
    
        
    for (s,v) in zip(samples, values):
        sums[s[0]:s[1],s[2]:s[3]] += v
        cnts[s[0]:s[1],s[2]:s[3]] += 1
        
    return sums / cnts

def combine_value_map_2d(samples, values):
    """
    Combines 2d value map for all mip-levels, using sample-values.
    :param samples: sample description
    :param values: corresponding values
    :returns: all 2d value map
    """
    def select_level(index):
        ps = [values[i] for i in range(len(samples)) if samples[i][0]==index]
        return [s[1:] for s in samples if s[0] == index], ps
    min_ind = min([s[0] for s in samples])
    max_ind = max([s[0] for s in samples])
    return [combine_value_map_2d_1level(*select_level(i)) for i in range(min_ind, max_ind+1)]
