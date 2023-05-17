import { FileState } from "./enums/fileState";
import { File } from "./file";

export interface FileBlank {
    name: string;
    path: string | null;
    url:string | null;
    base64: string;
    state: FileState;
}

export namespace FileBlank {
    export function create(name: string, base64: string, path: string | null = null, url:string |null = null,
         state: FileState | null = null): FileBlank {
        return {
            name: name,
            path: path,
            url: url,
            base64: base64,
            state: state ?? FileState.Added
        }
    }
    export function fromFile(file:File){
        return {
            name: "",
            path: file.path,
            url: file.url,
            base64: "",
            state: FileState.Intact
        }
    }
}