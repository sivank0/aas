export class Page<T>{
    constructor(
        public values: T[],
        public totalRows: number
    ) { }
}