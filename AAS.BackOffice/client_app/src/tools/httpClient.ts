import { InfrastructureLinks } from "../app/infrastructure/components/infrastructureLinks";

export class HttpClient {
    private static toQueryString(obj: any) {
        return obj
            ? `?${Object.keys(obj)
                .map(k => {
                    if (Array.isArray(obj[k]))
                        return (obj[k] as any[])
                            .map(val => `${encodeURIComponent(k)}=${encodeURIComponent(val)}`)
                            .join('&');

                    if (obj[k] instanceof Date)
                        return `${encodeURIComponent(k)}=${encodeURIComponent(obj[k].toISOString())}`;

                    return `${encodeURIComponent(k)}=${encodeURIComponent(obj[k])}`;
                }).join('&')}`
            : '';
    }

    public static getFile = async (fileUrl: string, data: any): Promise<Response> => {
        const headers = HttpClient.getHeaders();
        headers.append('Content-Type', 'application/json');

        const response = await fetch(fileUrl, {
            method: 'POST',
            headers,
            body: JSON.stringify(data)
        });
        if (!response.ok) throw new Error(`${response.status} - unknown status code`);
        return response;
    }

    public static getHeaders(): Headers {
        let headers: Headers = new Headers();
        headers.append('X-Requested-With', 'XMLHttpRequest');

        return headers;
    }

    public static async getJsonAsync(url: string, data?: any, host: string = ''): Promise<any> {
        let fullUrl = `${host}${url}${HttpClient.toQueryString(data)}`;
        const headers = HttpClient.getHeaders();

        const resp = await fetch(fullUrl, { method: 'GET', headers: headers });

        const response = await HttpClient.httpHandler(resp);

        if (response.status === 204) return null;

        return await response.json();
    }

    public static async postJsonAsync(url: string, data: any = null, params: any = null, host: string = ''): Promise<any> {
        const fullUrl = `${host}${url}${params != null ? HttpClient.toQueryString(params) : ''}`

        let headers: Headers = HttpClient.getHeaders();
        headers.append('Content-Type', 'application/json');

        const response = await HttpClient.httpHandler(
            await fetch(fullUrl,
                {
                    method: 'POST',
                    headers: headers,
                    body: JSON.stringify(data)
                }));

        return await response.json();
    }

    private static httpHandler(response: Response): Promise<Response> {
        if (response.redirected) {
            window.location.href = response.url;
            return Promise.reject();
        }

        if (response.ok) return Promise.resolve(response);

        switch (response.status) {
            case 403:
                window.location.href = InfrastructureLinks.forbidden
                return Promise.reject(new Error('Forbidden'));
            case 404:
                window.location.href = InfrastructureLinks.notFound;
                return Promise.reject(new Error('Not Found'));
        }

        return Promise.reject(`${response.status} - unknown status code`);
    }
}

export default HttpClient;