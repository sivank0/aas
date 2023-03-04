export const mathRound = (value: number, decimalCount: number): number => {
    const decimalMultiplier = 10;
    let totalDecimalMultiplier = 1;

    for (var i = 1; i <= decimalCount; i++) {
        totalDecimalMultiplier = totalDecimalMultiplier * decimalMultiplier;
    }

    return Math.round(value * totalDecimalMultiplier) / totalDecimalMultiplier;
}

export const mathCeil = (value: number, decimalCount: number): number => {
    const decimalMultiplier = 10;
    let totalDecimalMultiplier = 1;

    for (var i = 1; i <= decimalCount; i++) {
        totalDecimalMultiplier = totalDecimalMultiplier * decimalMultiplier;
    }

    return Math.ceil(value * totalDecimalMultiplier) / totalDecimalMultiplier;
}

export const mathFloor = (value: number, decimalCount: number): number => {
    const decimalMultiplier = 10;
    let totalDecimalMultiplier = 1;

    for (var i = 1; i <= decimalCount; i++) {
        totalDecimalMultiplier = totalDecimalMultiplier * decimalMultiplier;
    }

    return Math.floor(value * totalDecimalMultiplier) / totalDecimalMultiplier;
}

export const mathSum = (array: number[]): number => {
    return array.length > 0 ? array.reduce((first, second) => first + second) : 0;
}

/**
   * Возвращает слово в нужном падеже в зависимости от числа
   *
   * @param value - число
   * @param nominative - Именительный падеж. Например "День"
   * @param genitive - Родительный падеж. Например "Дня"
   * @param plural - Множественное число. Например "Дней"
   *
   */
export const decline = (value: number, nominative: string, genitive: string, plural: string): string => {
    value = value % 100;

    if (value >= 11 && value <= 19)
        return plural;

    value = value % 10;
    switch (value) {
        case 1: return nominative;

        case 2:
        case 3:
        case 4: return genitive;

        default: return plural;
    }
}

export const random = (min: number, max: number): number => {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

export const isNumber = (x: any) => +x === +x;
