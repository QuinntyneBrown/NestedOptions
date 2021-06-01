import { Preferences } from "./preferences";

export type User = {
    userId: string,
    username: string,
    isAdmin: boolean,
    preferences: Preferences
};
