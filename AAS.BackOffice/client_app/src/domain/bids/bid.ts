export class Bid {
    constructor(
        public readonly id: string,
        public readonly number: number,
        public readonly title: string,
        public readonly description: string,
        public readonly denyDescription: string | null,
        public readonly status: number,
        public readonly acceptanceDate: Date | null,
        public readonly approxmateDate: Date | null
    ) { }
}

export function toBid(value: any): Bid {
    return new Bid(
        value.id,
        value.number,
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
