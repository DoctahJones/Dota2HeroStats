import { Player } from './player.model';

export class AbilityDraftMatch {
    constructor(public MatchId?: number, public StartTime?: number, public DurationSeconds?: number, public ServerCluster?: number,
        public PatchNumber?: number, public SkillLevel?: number, public GameMode?: number, public DireKillScore?: number, public RadiantKillScore?: number,
        public RadiantWin?: boolean, public Players?: Player[]

    ) { }
}