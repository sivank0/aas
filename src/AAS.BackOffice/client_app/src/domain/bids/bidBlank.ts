import { FileBlank } from "../files/fileBlank"
import { Bid } from "./bid"
import { BidStatus } from "./bidStatus"

export interface BidBlank {
    id: string | null,
    title: string | null,
    description: string | null,
    denyDescription: string | null,
    status: BidStatus,
    acceptanceDate: Date | null,
    approximateDate: Date | null,
    fileBlanks: FileBlank[]
}

export namespace BidBlank {
    export function getDefault(): BidBlank {
        return {
            id: null,
            title: null,
            description: null,
            denyDescription: null,
            status: BidStatus.AwaitingVerification,
            acceptanceDate: null,
            approximateDate: null,
            fileBlanks: [],
        }
    }

    export function fromBid(bid: Bid): BidBlank {
        return {
            id: bid.id,
            title: bid.title,
            description: bid.description,
            denyDescription: bid.denyDescription,
            status: bid.status,
            acceptanceDate: bid.acceptanceDate,
            approximateDate: bid.approxmateDate,
            fileBlanks: []
        }
    }
}