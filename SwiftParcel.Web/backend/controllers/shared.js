exports.getPagination = (page, size) => {
    const limit = size ? +size : 3;
    const offset = page ? (page - 1) * limit : 0;

    return { limit, offset };
};

exports.getPagingData = (data, page, limit) => {
    const { count: total_results, rows: results } = data;
    const current_page = page ? +page : 1;
    const total_pages = Math.ceil(total_results / limit);

    return { page: current_page, results, total_pages, total_results };
};