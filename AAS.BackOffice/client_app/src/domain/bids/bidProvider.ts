import HttpClient from "../../tools/httpClient";
import { mapToPage, Page } from "../../tools/results/page";
import { mapToResult, Result } from "../../tools/results/result";
import { Bid, toBid, toBids } from "./bid";
import { BidBlank } from "./bidBlank";

export class BidsProvider {
    public static async saveBid(bidBlank: BidBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/bids/save", bidBlank);
        return mapToResult(result);
    }

    public static async getBidPage(page: number, countInPage: number): Promise<Page<Bid>> {
        const pagedBids = await HttpClient.getJsonAsync("/bids/get_page", { page, countInPage });
        return mapToPage<Bid>(pagedBids);
    }

    public static async removeBid(bidId: string): Promise<Result> {
        const result = await HttpClient.getJsonAsync("/bids/remove", { bidId });
        return mapToResult(result);
    }

    public static async getBidById(bidId: string): Promise<Bid> {
        const bid = await HttpClient.getJsonAsync("/bids/get_by_id", { bidId });
        return toBid(bid);
    }
}
