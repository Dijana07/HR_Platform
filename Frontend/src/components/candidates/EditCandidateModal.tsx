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
import { useState } from "react";
import type { SkillDTO } from "../../domain/DTOs/SkillDTO";
import { deleteCandidate, updateCandidate } from "../../api/candidateApi";

type EditCandidateModalProps = {
    candidate: CandidateDTO;
    open: boolean;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
    refreshCandidates: () => Promise<void>;
    refreshSkills: () => Promise<void>;
    skills: SkillDTO[];
};

export default function EditCandidateModal({candidate, open, setOpen, refreshCandidates, refreshSkills, skills} : EditCandidateModalProps) {
    const [candidateSkills, setCandidateSkills] = useState<SkillDTO[]>(candidate.skills || []);

    const handleUpdateCandidate = async () => {
        try {
            var res = await updateCandidate(candidate.id || 0, {
                candidateSkills
            });
            
            alert(res.message);
            if (res.success)
            {
                await refreshCandidates();
                setOpen(false);
            }
        } catch (error) {
            console.error("Error updating candidate:", error);
        }
    }

     const handleDeleteCandidate = async () => {
        try {
            if (window.confirm("Are you sure you want to delete this candidate? This action cannot be reversed."))
            {
                var res = await deleteCandidate(candidate.id || 0);

                alert(res.message);
                if (res.success)
                {
                    await refreshCandidates();
                    setOpen(false);
                }
            }
        } catch (error) {
            console.error("Error deleting candidate:", error);
        }
    }
    
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
                Edit candidate
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

                    <SkillsFilter 
                        skills={skills} 
                        selectedSkills={candidateSkills} 
                        onSkillSelect={setCandidateSkills} 
                        buttonName="Add new skill" 
                        refreshSkills={refreshSkills}/>
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
                <Button className="bg-[var(--text-name)]" isFullWidth onClick={handleUpdateCandidate}>
                    Update candidate
                </Button>
                <Typography type="p" className="mb-1 text-center">
                    or
                </Typography>
                <div className="flex justify-center">
                    <Button
                        color="error"
                        className="flex gap-2"
                        onClick={() => handleDeleteCandidate()}
                        >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="18"
                            height="18"
                            viewBox="0 0 24 24"
                            fill="none"
                            stroke="currentColor"
                            strokeWidth="2"
                            strokeLinecap="round"
                            strokeLinejoin="round"
                        >
                            <path d="M3 6h18" />
                            <path d="M8 6V4h8v2" />
                            <path d="M19 6l-1 14H6L5 6" />
                            <path d="M10 11v6" />
                            <path d="M14 11v6" />
                        </svg>

                        Delete
                    </Button>
                </div>
            </form>
            </Dialog.Content>
        </Dialog.Overlay>
        </Dialog>
    );
}
