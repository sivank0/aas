import { ErrorData, mapToErrorData } from './errorData';

export class Result {
    public isSuccess = this.errors.length === 0;

    constructor(
        public errors: ErrorData[]
    ) { }

    public static success<T>(): Result {
        return new Result([]);
    }

    public static fail(errors: ErrorData[]): Result {
        return new Result(errors);
    }

    public getErrorsString = (): string => {
        return this.errors.map(error => error.message).join('. ')
    }
}

export const mapToResult = (result: any): Result => {
    if (!result.isSuccess)
        return Result.fail(mapToErrorData(result.errors))
    return Result.success();
}
