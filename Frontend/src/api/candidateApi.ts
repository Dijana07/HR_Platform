import { apiClient } from "./clientApi";

export const getCandidates = async () => {
  return await apiClient("/Candidate", 
  {
    method: "GET",
  });
}

export const createCandidate = async (candidateData: any) => {
  var res = await apiClient("/Candidate", 
  {
    method: "POST",
    body: JSON.stringify(candidateData),
  });
  console.log(res);
  return res;
}   

export const updateCandidate = async (id: number, candidateData: any) => {
    console.log(candidateData.candidateSkills);
  return await apiClient(`/Candidate/${id}`, 
  {
    method: "PUT",
    body: JSON.stringify(candidateData.candidateSkills),
    });
}

export const deleteCandidate = async (id: number) => {
    return await apiClient(`/Candidate/${id}`,
    {
        method: "DELETE",
    });
}

export const searchCandidates = async (query?: string, skills?: string[]) => {
    const params = new URLSearchParams();

    if (query) {
        params.append("name", query);
    }

    skills?.forEach((skill) => {
        params.append("skills", skill);
    });

    return await apiClient(
        `/Candidate/search?${params.toString()}`,
        {
            method: "GET",
        }
    );
}



