package test_mnist

class LayerConv2D extends CompNode {
}

/*
Forward:
batch_size x num_rows x num_cols x in_features
apply convolution of out_features x k_rows x k_cols x in_feature (with out_features biases)
with stride_rows=1, stride_cols=1

dst(row, col, feature) = sum(-d to +d, -d to -d){sum_j(src(row+irow, col+icol, j} * filter(irow, icol, j)}

dL / dsrc = dL / ddst * (ddst / dsrc)

ddst(row1, col1, f1) / dsrc(row2, col2, f2) =
=
*/