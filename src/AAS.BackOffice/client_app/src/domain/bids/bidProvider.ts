import HttpClient from "../../tools/httpClient";
import { mapToResult, Result } from "../../tools/types/results/result";
import { Bid, toBid, toBids } from "./bid";
import { BidBlank } from "./bidBlank";
import { BidStatus } from "./bidStatus";

export class BidsProvider {
    public static async saveBid(bidBlank: BidBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/bids/save", bidBlank);
        return mapToResult(result);
    }

    public static async getBidsAll(): Promise<Bid[]> {
        const bids = await HttpClient.getJsonAsync("/bids/get_all");
        return toBids(bids);
    }

    public static async getBidById(bidId: string): Promise<Bid> {
        const bid = await HttpClient.getJsonAsync("/bids/get_by_id", { bidId });
        return toBid(bid);
    }

    public static async getBidsBySearch(searchableText: string): Promise<Bid[]> {
        const bids = await HttpClient.getJsonAsync("/bids/get_by_search", { searchableText });
        return toBids(bids);
    }

    public static async changeBidDenyDescription(bidId: string, bidDenyDescription: string | null): Promise<Result> {
        const result = await HttpClient.getJsonAsync('/bids/change_bid_deny_description', { bidId, bidDenyDescription });
        return mapToResult(result);
    }

    public static async changeBidStatus(bidId: string, bidStatus: BidStatus): Promise<Result> {
        const result = await HttpClient.getJsonAsync('/bids/change_bid_status', { bidId, bidStatus });
        return mapToResult(result);
    }

    public static async removeBid(bidId: string): Promise<Result> {
        const result = await HttpClient.getJsonAsync("/bids/remove", { bidId });
        return mapToResult(result);
    }
}
