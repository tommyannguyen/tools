import { StringMap } from "../support/Support";
export declare type JSONSchema = StringMap | boolean;
export declare abstract class JSONSchemaStore {
    private readonly _schemas;
    private add(address, schema);
    abstract fetch(_address: string): Promise<JSONSchema | undefined>;
    get(address: string, debugPrint: boolean): Promise<JSONSchema | undefined>;
}
