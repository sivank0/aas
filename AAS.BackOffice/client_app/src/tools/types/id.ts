import {v4 as uuid} from 'uuid';

export const newId = () => uuid().replace(/-/g, "").toUpperCase();
