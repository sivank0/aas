import { FileArea } from "./enums/fileArea";
import { FileState } from "./enums/fileState";
import { File } from "./file";

export interface FileBlank {
    name: string;
    path: string | null;
    url: string | null;
    base64: string;
    area: FileArea
    state: FileState;
}

export namespace FileBlank {
    export function create(name: string, base64: string, area: FileArea, path: string | null = null, url: string | null = null,
        state: FileState | null = null): FileBlank {
        return {
            name: name,
            path: path,
            url: url,
            base64: base64,
            area: area,
            state: state ?? FileState.Added
        }
    }

    export function fromFile(file: File): FileBlank {
        const filePathParts = file.path.split('/')
        const fileName = filePathParts[filePathParts.length - 1];

        return {
            name: fileName,
            path: file.path,
            url: file.url,
            base64: "",
            area: FileArea.getFileArea(file.path),
            state: FileState.Intact
        }
    }
}