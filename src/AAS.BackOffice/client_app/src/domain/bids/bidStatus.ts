export enum BidStatus {
    AwaitingVerification = 1,
    Denied = 2,
    InWork = 3,
    Completed = 4
}

export namespace BidStatus {
    export function getDisplayName(status: BidStatus) {
        switch (status) {
            case BidStatus.AwaitingVerification:
                return "Ожидает проверки";
            case BidStatus.Denied:
                return "Отклонена";
            case BidStatus.InWork:
                return "В работе";
            case BidStatus.Completed:
                return "Завершена";
        }
    }
}