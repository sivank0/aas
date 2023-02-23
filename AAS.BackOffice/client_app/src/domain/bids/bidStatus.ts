export enum BidStatus {
    Created = 1,
    AwaitingVerification = 2,
    Denied = 3,
    InWork = 4,
    Completed = 5
}

export namespace BidStatus {
    export function getDisplayName(status: BidStatus) {
        switch (status) {
            case BidStatus.Created: return "Создана";
            case BidStatus.AwaitingVerification: return "Ожидает проверки";
            case BidStatus.Denied: return "Отклонена";
            case BidStatus.InWork: return "В работе";
            case BidStatus.Completed: return "Завершена";
        }
    }
}