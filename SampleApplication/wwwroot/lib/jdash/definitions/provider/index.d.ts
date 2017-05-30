import { HtmlElement, KeyValue } from '../core';
import { IClientProvider } from 'jdash-core';
export declare class ProviderManager {
    static providers: KeyValue<Function>;
    static get(type?: string): any;
    static register(type: string, provider: any): IClientProvider;
}
export declare class ProviderElement extends HtmlElement {
    provider: IClientProvider;
    static readonly observedAttributes: string[];
    static locate(id?: string): ProviderElement;
    constructor();
    createProvider(constructor: typeof Object, params: any): void;
    connectedCallback(): void;
}
