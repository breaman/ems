export const TEAMS_LOADED = '[Teams] Loaded';
export const TEAMS_GET_ALL = '[Teams] Get All';
export const TEAMS_ERROR = '[Teams] Error';
export const TEAM_CREATE = '[Team] Create';
export const TEAM_UPDATE = '[Team] Update';
export const TEAM_DELETE = '[Team] Delete';
export const TEAM_ERROR = '[Team] Error';

export class Team {
    name: string | undefined;
    division: string | undefined;
    gender: string | undefined;
    competitionLevel: string | undefined;
}

export interface TeamState {
    teams?: Array<Team>;
    error: boolean;
}