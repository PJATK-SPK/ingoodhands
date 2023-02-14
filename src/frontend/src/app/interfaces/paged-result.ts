export interface PagedResult<T> {
    queryable: T[];
    currentPage: number;
    pageCount: number;
    pageSize: number;
    rowCount: number;
}