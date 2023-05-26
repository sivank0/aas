export enum FileArea {
    Unknown = 1,
    User = 2,
    Bid = 3
}

export namespace FileArea {
    export function getFileArea(filePath: string) {
        if (filePath.includes("Bids"))
            return FileArea.Bid

        if (filePath.includes("Users"))
            return FileArea.User;

        return FileArea.Unknown;
    }
}