import { useState } from "react";
import type { SkillDTO } from "../../domain/SkillDTO";

type SkillsFilterProps = {
    skills: SkillDTO[];
    selectedSkills: SkillDTO[];
    onSkillSelect: (skills: SkillDTO[]) => void;
    buttonName?: string;
    onButtonClick?: (skills: SkillDTO[]) => void;
};

export default function SkillsFilter({ skills, selectedSkills, onSkillSelect, buttonName, onButtonClick }: SkillsFilterProps) {
    const [open, setOpen] = useState(false);
    
    return (
        <>
        <div className="relative">
        <button
            id="dropdownDefault"
            data-dropdown-toggle="dropdown"
            className="text-white bg-black font-medium rounded-lg text-sm px-4 py-2.5 inline-flex"
            type="button"
            onClick={() => setOpen(!open)}
        >
            {buttonName === "Apply" ? "Filter skills" : "Select skills"}

            <svg
                className="w-4 h-4 ml-2"
                aria-hidden="true"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
                xmlns="http://www.w3.org/2000/svg"
            >
                <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    d="M19 9l-7 7-7-7"
                ></path>
            </svg>
        </button>

        <div
            id="dropdown"
            className={`${open ? "block" : "hidden" } absolute top-full right-0 mt-0 z-10 w-60 p-4 bg-white rounded-lg shadow border border-gray-400`}
        >
            <h6 className="mb-3 text-sm font-medium text-gray-900 dark:text-white">
                Skills
            </h6>

            <ul className="space-y-2 text-sm text-left" aria-labelledby="dropdownDefault">
                {skills.map((skill) => (
                    <li key={skill.id} className="flex !justify-start !items-start gap-2 ">
                        <input
                            id={skill.name}
                            type="checkbox"
                            value=""
                            className="mt-1 w-4 h-4 bg-gray-100 border-gray-300 rounded text-primary-600 focus:ring-primary-500 dark:focus:ring-primary-600 dark:ring-offset-gray-700 focus:ring-2 dark:bg-gray-600 dark:border-gray-500"
                            checked={selectedSkills.some((s) => s.id === skill.id)}
                            onChange={(e) => {
                                if (e.target.checked) {
                                    onSkillSelect([...selectedSkills, skill]);
                                } else {
                                    onSkillSelect(selectedSkills.filter((s) => s.id !== skill.id));
                                }
                            }}
                        />

                        <label
                            htmlFor={skill.name}
                            className="text-left w-full text-sm font-medium text-gray-900 dark:text-gray-100"
                            >
                            {skill.name}
                        </label>
                    </li>
                ))}
            </ul>

            {buttonName === "Add new skill" && (
            <button
                
                className="w-40 text-white bg-black hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-4 py-2.5 mt-4"
                onClick={() => {
                    setOpen(false);
                    onButtonClick?.(selectedSkills);
                }}
            >
                Add new skill
            </button>
            )}
        </div>
        </div>
        </>
    );
}