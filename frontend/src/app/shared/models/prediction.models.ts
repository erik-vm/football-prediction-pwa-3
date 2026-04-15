export interface PredictionDto { id: string; userId: string; username: string; matchId: string; homeTeam: string; awayTeam: string; kickoffTime: string; predictedHome: number; predictedAway: number; pointsEarned: number | null; actualHome: number | null; actualAway: number | null; matchFinished: boolean; createdAt: string; updatedAt: string; }
export interface SubmitPredictionRequest { matchId: string; homeScore: number; awayScore: number; }
export interface UpdatePredictionRequest { homeScore: number; awayScore: number; }
