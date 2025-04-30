export interface ResponseBase<T> {
  isSuccessful: boolean;
  statusCode: number;
  message: string | null;
  data: T;
}
