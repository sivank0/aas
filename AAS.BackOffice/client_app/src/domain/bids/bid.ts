export class Bid {
    constructor(
        public readonly id: string,
        public readonly title: string,
        public readonly description: string,
        public readonly deynDescription: string | null,
        public readonly status: string,
        public readonly acceptanceDate: Date | null,
        public readonly approxmateDate: Date | null
    ) { }
}

export function toBid(value: any): Bid {
    return new Bid(
        value.id,
        value.title,
        value.description,
        value.denyDescription,
        value.status,
        value.acceptanceDate,
        value.approxmateDate,
    )
}
export function toBids(values: any[]): Bid[] {
    return values.map(toBid);
}
