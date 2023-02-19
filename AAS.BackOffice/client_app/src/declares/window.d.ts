import { BrowserType } from "../tools/browserType/browserType"

export { }

declare global {
    interface Window {
        browserType: BrowserType
    }
}
