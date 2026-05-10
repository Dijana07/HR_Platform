import type { SkillDTO } from "../SkillDTO";

export interface CandidateDTO {
    id?: number;
    name: string;
    dateOfBirth: Date;
    email: string;
    contactNumber: string;
    skills?: SkillDTO[];
}