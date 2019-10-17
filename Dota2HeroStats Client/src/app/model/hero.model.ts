export class Hero {
    constructor(public HeroId?: number, public Name?: string, public LocalizedName?: string,
        public PrimaryAttr?: string, public AttackType?: string, public Legs?: number, public Roles?: string[]) {
    }

    getImage(size?: string): string {
        var name = this.Name.replace("npc_dota_hero_", "");
        switch (size) {
            case "large":
                name += "_full.png"
                break;
            case "medium":
                name += "_lg.png"
                break;
            case "small":
                name += "_sb.png"
                break;
            default:
                name += "_sb.png"
        }
        return `http://cdn.dota2.com/apps/dota2/images/heroes/${name}`;
    }

    createRoleString() {
        return this.Roles.join(", ");
    }
}