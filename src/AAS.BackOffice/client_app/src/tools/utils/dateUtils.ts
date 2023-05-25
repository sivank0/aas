export namespace DateUtils {
    export const getDateWithOutTime = (date: Date) => {
        return new Date(date.getFullYear(), date.getMonth(), date.getDate(), 0, 0, 0, 0)
    }

    export const getUtcAsLocal = (date: Date) => {
        return new Date(date.getTime() - date.getTimezoneOffset() * 60000)
    }

    export function getUtcAsLocalWithoutTime(date: Date) {
        return getUtcAsLocal(getDateWithOutTime(date))
    }

    export const getLocalAsUtc = (date: Date) => {
        return new Date(date.getTime() + date.getTimezoneOffset() * 60000)
    }

    export function getLocalAsUtcWithoutTime(date: Date) {
        return getDateWithOutTime(getLocalAsUtc(date))
    }
}