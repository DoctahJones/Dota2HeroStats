export class Ability {
    constructor(public AbilityId?: number, public Name?: string, public Img?: string,
        public IsUltimate?: boolean, public OriginalAbilityOwnerId?: number) {
    }

    getImage(): string {
        return "https://www.dotabuff.com/assets/skills/" + this.Img;
    }
}