import { apiClient } from "./clientApi";

export const getSkills = async () => {
  return await apiClient("/Skill", 
  {
    method: "GET",
  });
}

export const getSkillById = async (id: number) => {
  return await apiClient(`/Skill/${id}`, 
  {
    method: "GET",
  });
}

export const createSkill = async (skillData: any) => {
  return await apiClient("/Skill", 
  {
    method: "POST",
    body: JSON.stringify(skillData),
  });
}   
