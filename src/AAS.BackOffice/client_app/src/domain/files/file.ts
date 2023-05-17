export class File {
    constructor(
        public readonly path: string,
        public readonly url: string | null
    ){}
}

export function mapToFile(value: any){
    return new File(value.path, value.url);
}