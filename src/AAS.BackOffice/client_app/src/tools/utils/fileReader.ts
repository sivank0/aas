const mimeTypeByExtension = new Map<string, string>
    ([
        ['docx', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'],
        ['doc', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'],
    ])

export function readAsDataUrl(file: File): Promise<string> {
    return new Promise<string>((resolve, reject) => {
        const reader = new FileReader()
        reader.onload = () => {
            const extensionIndex = file.name.lastIndexOf('.') + 1
            const extension = extensionIndex > 0 ? file.name.substring(extensionIndex) : null

            if (extension === null) return;

            const mimeType = mimeTypeByExtension.get(extension)
            let dataUrl: string | null = reader.result as string

            if (mimeType != null) dataUrl = mapToDataUrl(dataUrl, mimeType);

            if (dataUrl === null) return;

            resolve(dataUrl)
        }
        reader.onerror = () => reject(reader.result)

        reader.readAsDataURL(file);
    });
}

export function readAsText(file: File): Promise<string> {
    return new Promise<string>((resolve, reject) => {
        const reader = new FileReader()
        reader.onload = () => resolve(reader.result as string)
        reader.onerror = () => reject(reader.result)

        reader.readAsText(file, 'Windows-1251')
    });
}

export function readAsBase64(file: File): Promise<string> {
    return new Promise<string>((resolve, reject) => {
        const reader = new FileReader()
        reader.onload = () => resolve((reader.result as string).replace("data:", "").replace(/^.+,/, ""))
        reader.onerror = () => reject(reader.result)

        reader.readAsDataURL(file);
    });
}

export function mapToDataUrl(base64: string | null, contentType: string | null): string | null {
    if (base64 === null || contentType === null) return null;

    if (base64.includes(`data:${contentType};base64,`))
        return base64;

    return `data:${contentType};base64,${base64}`
}