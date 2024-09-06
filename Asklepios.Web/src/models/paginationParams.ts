export interface PaginationParams {
  PageIndex: number;
  PageSize: number;
}

export class InputPagination {
  PageIndex: number = 1;
  PageSize: number = 10;
}
