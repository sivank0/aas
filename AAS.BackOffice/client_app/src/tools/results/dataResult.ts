import { ErrorData, mapToErrorData } from './errorData';

export class DataResult<T> {
    public isSuccess = this.errors.length === 0;

    constructor(
        public data: T | null,
        public errors: ErrorData[]
    ) { }

    public static success<T>(value: T): DataResult<T> {
        return new DataResult<T>(value, []);
    }

    public static fail<T>(errors: ErrorData[]): DataResult<T> {
        return new DataResult<T>(null, errors);
    }

    public getErrorsString = (): string => {
        return this.errors.map(error => error.message).join('. ')
    }
}

export function mapToDataResult<T>(result: any): DataResult<T> {
    if (!result.isSuccess) return DataResult.fail<T>(mapToErrorData(result.errors))

    return DataResult.success(result.data);
}
