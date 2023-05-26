const contentTypesForOpen = ["image/jpeg", "image/png", "image/jpg", "application/pdf"];

export abstract class FileDetails {
    constructor(
        public readonly name: string,
        public readonly extension: string,
        public readonly contentType: string | null,
        public readonly path: string | null
    ) { }
}

export class FileDetailsOfBytes extends FileDetails {
    constructor(
        name: string,
        extension: string,
        contentType: string | null,
        path: string | null,
        public readonly bytes: ArrayBuffer,
    ) { super(name, extension, contentType, path); }
}

export class FileDetailsOfBase64 extends FileDetails {
    constructor(
        name: string,
        extension: string,
        contentType: string | null,
        path: string | null,
        public readonly base64: string
    ) { super(name, extension, contentType, path); }
}

export function mapToFileDetailsOfBase64(data: any): FileDetailsOfBase64 {
    return new FileDetailsOfBase64(data.name, data.extension, data.contentType, data.path, data.base64);
}

export function mapToArrayOfFileDetailsOfBase64(values: any[]): FileDetailsOfBase64[] {
    return values.map(mapToFileDetailsOfBase64);
}