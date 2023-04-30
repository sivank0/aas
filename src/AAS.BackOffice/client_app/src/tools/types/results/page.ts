export class Page<T> {
    constructor(
        public values: T[],
        public totalRows: number
    ) {
    }
}

export function mapToPage<T>(value: any): Page<T> {
    return new Page<T>(value.values, value.totalRows);
}