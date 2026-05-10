import {
  Dialog,
  Button,
  Input,
  Typography,
  IconButton,
} from "@material-tailwind/react";
import { Xmark } from "iconoir-react";
import type { CandidateDTO } from "../../domain/DTOs/CandidateDTO";
import { SkillBadge } from "../skills/SkillBadge";
import SkillsFilter from "../skills/SkillsFilter";
import { useEffect, useState } from "react";
import type { SkillDTO } from "../../domain/SkillDTO";
import { getSkills } from "../../api/skillApi";
import { updateCandidate } from "../../api/candidateApi";

type EditCandidateModalProps = {
    candidate: CandidateDTO;
    open: boolean;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
    refreshCandidates: () => Promise<void>;
};

export default function EditCandidateModal({candidate, open, setOpen, refreshCandidates} : EditCandidateModalProps) {
    const [skills, setSkills] = useState<SkillDTO[]>([]);
    const [candidateSkills, setCandidateSkills] = useState<SkillDTO[]>(candidate.skills || []);

    const handleUpdateCandidate = async () => {
        try {
            await updateCandidate(candidate.id || 0, {
                candidateSkills
            });
            await refreshCandidates();
            setOpen(false);
            alert("Candidate updated successfully!");
        } catch (error) {
            console.error("Error updating candidate:", error);
        }
    }

    useEffect(() => {
        const fetchSkills = async () => {
            try {
                const skillsData = await getSkills();
                setSkills(skillsData);
            } catch (error) {
                console.error("Error fetching skills:", error);
            }   
        };
        fetchSkills();
    }, []);
    
    return (
        <Dialog size="sm" open={open}>
        <Dialog.Overlay>
            <Dialog.Content>
            <Dialog.DismissTrigger
                as={IconButton}
                size="sm"
                variant="ghost"
                color="secondary"
                className="absolute right-2 top-2"
                isCircular
                onClick={() => setOpen(false)}
            >
                <Xmark className="h-5 w-5" />
            </Dialog.DismissTrigger>
            <Typography type="h6" className="mb-1">
                Edit Candidate
            </Typography>
            <Typography className="text-foreground">
                Update candidate details below.
            </Typography>
            <form action="#" className="mt-6">
                <div className="mb-4 mt-2 space-y-1.5">
                <Typography
                    as="label"
                    htmlFor="name"
                    type="small"
                    color="default"
                    className="font-semibold"
                >
                    Name:
                </Typography>
                <Input
                    id="name"
                    type="text"
                    value={candidate.name}
                    disabled
                />

                <Typography
                    as="label"
                    htmlFor="email"
                    type="small"
                    color="default"
                    className="font-semibold"
                >
                    Email
                </Typography>
                <Input
                    id="email"
                    type="email"
                    value={candidate.email}
                    disabled
                />

                <Typography
                    as="label"
                    htmlFor="dateOfBirth"
                    type="small"
                    color="default"
                    className="font-semibold"
                >
                    Date of Birth
                </Typography>
                <Input
                    id="dateOfBirth"
                    type="date"
                    value={
                        candidate.dateOfBirth
                            ? new Date(candidate.dateOfBirth).toISOString().split("T")[0]
                            : ""
                    }
                    disabled
                />
                </div>
                
                <div className="mb-4 space-y-1.5">

                <Typography
                    as="label"
                    htmlFor="contactNumber"
                    type="small"
                    color="default"
                    className="font-semibold"
                >
                    Contact Number
                </Typography>
                <Input
                    id="contactNumber"
                    type="text"
                    value={candidate.contactNumber}
                    disabled
                />

                <div className="flex !space-x-28 space-y-4 mb-4 mt-2">
                    <Typography
                        as="label"
                        htmlFor="skills"
                        type="small"
                        color="default"
                        className="font-semibold mt-5"
                    >
                        Skills
                    </Typography>

                    <SkillsFilter skills={skills} selectedSkills={candidateSkills} 
                        onSkillSelect={setCandidateSkills} buttonName="Add new skill"/>
                </div>
                <div className="flex flex-wrap gap-2">
                    {candidateSkills?.length ? (
                    candidateSkills.map((skill) => (
                        <SkillBadge key={skill.id} skill={skill} />
                    ))
                    ) : (
                    <span>None</span>
                    )}
                </div>
                </div>
                <Button isFullWidth onClick={handleUpdateCandidate}>
                    Update Candidate
                </Button>
            </form>
            </Dialog.Content>
        </Dialog.Overlay>
        </Dialog>
    );
}
