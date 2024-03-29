import { FileBlank } from "../files/fileBlank"
import { Bid } from "./bid"
import { BidStatus } from "./bidStatus"

export interface BidBlank {
    id: string | null,
    number: number | null,
    title: string | null,
    description: string | null,
    denyDescription: string | null,
    status: BidStatus,
    acceptanceDate: Date | null,
    approximateDate: string | Date | null | undefined,
    fileBlanks: FileBlank[],
    createdUserId: string
}

export namespace BidBlank {
    export function getDefault(systemUserId: string): BidBlank {
        return {
            id: null,
            number: null,
            title: null,
            description: null,
            denyDescription: null,
            status: BidStatus.AwaitingVerification,
            acceptanceDate: null,
            approximateDate: null,
            fileBlanks: [],
            createdUserId: systemUserId
        }
    }

    export function fromBid(bid: Bid): BidBlank {
        return {
            id: bid.id,
            number: bid.number,
            title: bid.title,
            description: bid.description,
            denyDescription: bid.denyDescription,
            status: bid.status,
            acceptanceDate: bid.acceptanceDate,
            approximateDate: bid.approximateDate,
            fileBlanks: bid.files.map(file => FileBlank.fromFile(file)),
            createdUserId: bid.createdUserId
        }
    }
}