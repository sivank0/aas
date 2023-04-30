import { FileState } from "./enums/fileState";

export interface FileBlank {
    name: string;
    path: string | null;
    base64: string;
    state: FileState;
}

export namespace FileBlank {
    export function create(name: string, base64: string, path: string | null = null, state: FileState | null = null): FileBlank {
        return {
            name: name,
            path: path,
            base64: base64,
            state: state ?? FileState.Added
        }
    }
}