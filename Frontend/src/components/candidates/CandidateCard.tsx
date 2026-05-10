import type { CandidateDTO } from "../../domain/DTOs/CandidateDTO";
import { SkillBadge } from "../skills/SkillBadge";
import {
  Card,
  CardBody,
  CardFooter,
  Typography,
} from "@material-tailwind/react";

type CandidateCardProps = {
  candidate: CandidateDTO;
  onDoubleClicked: (e: React.MouseEvent)  => void;
};

export default function CandidateCard({ candidate, onDoubleClicked }: CandidateCardProps) {
  return (
    <Card onDoubleClick={onDoubleClicked} className="w-full rounded-3xl border-2 border-gray-200 bg-white p-2 shadow-lg">
      <CardBody className="p-4 pb-0 text-left">
        <Typography variant="h5" className="mb-3 text-slate-800">
          {candidate.name}
        </Typography>

        <Typography className="my-1 text-gray-700">
          Date of Birth:{" "}
          {new Date(candidate.dateOfBirth).toLocaleDateString()}
        </Typography>

        <Typography className="my-1 text-gray-700">
          Email: {candidate.email}
        </Typography>

        <Typography className="my-1 text-gray-700">
          Contact Number: {candidate.contactNumber}
        </Typography>
      </CardBody>

      <CardFooter className="pt-0 text-left">
        <Typography className="mb-2 text-gray-800">
          Skills:
        </Typography>

        <div className="flex flex-wrap gap-2">
          {candidate.skills?.length ? (
            candidate.skills.map((skill) => (
              <SkillBadge key={skill.id} skill={skill} />
            ))
          ) : (
            <span>None</span>
          )}
        </div>
      </CardFooter>
    </Card>
  );
}