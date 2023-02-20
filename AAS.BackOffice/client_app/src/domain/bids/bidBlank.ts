import { Bid } from "./bid"

export interface BidBlank {
    id: string | null,
    title: string | null,
    description: string | null,
    denyDescription: string | null,
    status: string | null,
    acceptanceDate: Date | null,
    approximateDate: Date | null,
}

export namespace BidBlank {
    export function getDefault(): BidBlank {
        return {
            id: null,
            title: null,
            description: null,
            denyDescription: null,
            status: null,
            acceptanceDate: null,
            approximateDate: null,
        }
    }

    export function fromBid(bid: Bid): BidBlank {
        return {
            id: bid.id,
            title: bid.title,
            description: bid.description,
            denyDescription: bid.deynDescription,
            status: bid.status,
            acceptanceDate: bid.acceptanceDate,
            approximateDate: bid.approxmateDate,
        }
    }
}