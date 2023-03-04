import { BrowserType } from "../tools/browserType"

export { }

declare global {
    interface Window {
        browserType: BrowserType
    }
}
