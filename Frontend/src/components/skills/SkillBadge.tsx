import type { SkillDTO } from "../../domain/SkillDTO";
import { Chip } from "@material-tailwind/react";

export const SkillBadge: React.FC<{ skill: SkillDTO }> = ({ skill }) => {
  return <Chip variant="outline" className="p-1 m-1"> {skill.name} </Chip>;
};