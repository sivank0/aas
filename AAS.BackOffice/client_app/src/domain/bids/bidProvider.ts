import HttpClient from "../../tools/httpClient";
import { mapToResult, Result } from "../../tools/results/result";
import { Bid, toBids } from "./bid";
import { BidBlank } from "./bidBlank";

export class BidsProvider {
    public static async saveBid(BidBlank: BidBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("bids/save", BidBlank);
        return mapToResult(result);
    }

    public static async getBids(): Promise<Bid[]> {
        const bids = await HttpClient.getJsonAsync("bids/get_all");
        return toBids(bids);
    }

    public static async removeBid(bidId: string): Promise<Result> {
        const result = await HttpClient.getJsonAsync("bids/remove", { bidId });
        return mapToResult(result);
    }

}
