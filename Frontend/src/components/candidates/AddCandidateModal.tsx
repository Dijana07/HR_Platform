import {
  Dialog,
  Button,
  Input,
  Typography,
  IconButton,
} from "@material-tailwind/react";
import { Xmark } from "iconoir-react";
import SkillsFilter from "../skills/SkillsFilter";
import React, { useState } from "react";
import type { SkillDTO } from "../../domain/SkillDTO";
import { SkillBadge } from "../skills/SkillBadge";
import { createCandidate } from "../../api/candidateApi";

type AddCandidateModalProps = {
    refreshCandidates: () => Promise<void>;
    open: boolean;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
    refreshSkills: () => Promise<void>;
    skills: SkillDTO[];
}

export default function AddCandidateModal({open, setOpen, refreshCandidates, refreshSkills, skills} : AddCandidateModalProps) {
    const [selectedSkills, setSelectedSkills] = useState<SkillDTO[]>([]);
    const [formData, setFormData] = useState({
        name: "",
        email: "",
        dateOfBirth: "",
        contactNumber: "",
    });

    const handleAddCandidate = async () => {
        try {
            if (formData.name && formData.email && formData.dateOfBirth && formData.contactNumber) {
                await createCandidate({
                    ...formData,
                    skills: selectedSkills
                });

                await refreshCandidates();
                setOpen(false);
                alert("Candidate added successfully!");
            } else {
                alert("Please fill in all required fields.");
            }
        } catch (error) {
            console.error("Error adding candidate:", error);
        }
    };

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
                Add Candidate
            </Typography>
            <Typography className="text-foreground">
                Enter candidate details to add them to the platform.
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
                    Name
                </Typography>
                <Input
                    id="name"
                    type="text"
                    placeholder="John Doe"
                    value={formData.name}
                    onChange={(e) => setFormData({...formData, name: e.target.value})}
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
                    placeholder="someone@example.com"
                    required
                    value={formData.email}
                    onChange={(e) => setFormData({...formData, email: e.target.value})}
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
                    placeholder="1990-01-01"
                    required
                    value={formData.dateOfBirth}
                    onChange={(e) => setFormData({...formData, dateOfBirth: e.target.value})}
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
                    placeholder="123-456-7890"
                    required
                    value={formData.contactNumber}
                    onChange={(e) => setFormData({...formData, contactNumber: e.target.value})}
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
                        selectedSkills={selectedSkills} 
                        onSkillSelect={setSelectedSkills} 
                        buttonName="Add new skill" 
                        refreshSkills={refreshSkills}/>
                </div>
                <div className="flex flex-wrap gap-2">
                    {selectedSkills.length ? (
                    selectedSkills.map((skill) => (
                        <SkillBadge key={skill.id} skill={skill} />
                    ))
                    ) : (
                    <span>None</span>
                    )}
                </div>
                </div>
                <Button isFullWidth onClick={handleAddCandidate}>
                    Add Candidate
                </Button>
            </form>
            </Dialog.Content>
        </Dialog.Overlay>
        </Dialog>
    );
}
