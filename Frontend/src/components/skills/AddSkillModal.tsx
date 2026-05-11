import {
  Dialog,
  Button,
  Input,
  Typography,
  IconButton,
} from "@material-tailwind/react";
import { Xmark } from "iconoir-react";
import React, { useState } from "react";
import { createSkill } from "../../api/skillApi";

type AddSkillModalProps = {
    refreshSkills: () => Promise<void>;
    open: boolean;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

export default function AddSkillModal({open, setOpen, refreshSkills} : AddSkillModalProps) {
    const [formData, setFormData] = useState({
        name: ""
    });

    const handleAddSkill = async () => {
        try {
            if (formData.name) {
                if (!formData.name.trim()) {
                    alert("Skill name is required.");
                    return;
                }
                
                const res = await createSkill({
                    ...formData
                });

                alert(res.message);
                if (res.success)
                {
                    await refreshSkills();
                    setOpen(false);
                }
            } else {
                alert("Please fill in all required fields.");
            }
        } catch (error) {
            console.error("Error adding skill:", error);
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
                Add skill
            </Typography>
            <Typography className="text-foreground">
                Enter skill name to add them to the platform.
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
                        placeholder="eg. English, C# programming..."
                        value={formData.name}
                        onChange={(e) => setFormData({...formData, name: e.target.value})}
                    />
                </div>
                <Button isFullWidth onClick={handleAddSkill}>
                    Add skill
                </Button>
            </form>
            </Dialog.Content>
        </Dialog.Overlay>
        </Dialog>
    );
}
