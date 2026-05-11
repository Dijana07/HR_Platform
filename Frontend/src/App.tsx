import { useEffect, useState } from 'react'
import './App.css'
import CandidateCard from './components/candidates/CandidateCard'
import { getCandidates, searchCandidates } from './api/candidateApi'
import { getSkills } from './api/skillApi'
import AddCandidateModal from './components/candidates/AddCandidateModal'
import SkillsFilter from './components/skills/SkillsFilter'
import EditCandidateModal from './components/candidates/EditCandidateModal'
import type { SkillDTO } from './domain/DTOs/SkillDTO'
import { Button } from '@material-tailwind/react'

function App() {
  const [candidates, setCandidates] = useState([])
  const [skills, setSkills] = useState([])
  const [selectedCandidate, setSelectedCandidate] = useState<any>(null);
  const [openEditModal, setOpenEditModal] = useState(false);
  const [openAddModal, setOpenAddModal] = useState(false);
  const [selectedSkills, setSelectedSkills] = useState<SkillDTO[]>([]);
  const [query, setQuery] = useState('');

  const onDoubleClickedCandidate = (candidate: any) => {
    setSelectedCandidate(candidate);
    setOpenEditModal(true);
  };

  useEffect(() => {
    const fetchFilteredCandidates = async () => {
      const response = await searchCandidates(
        query,
        selectedSkills.map((s) => s.name)
      );

      setCandidates(response || []);
    };

    fetchFilteredCandidates();
  }, [query, selectedSkills]);

  const fetchCandidates = async () => {
    const response = await getCandidates();
    setCandidates(response || []);
  };

  const fetchSkills = async () => {
    const response = await getSkills();
    setSkills(response || []);
  };

  useEffect(() => {
    fetchCandidates();    
    fetchSkills();
  }, []);

  const renderCandidates = () => {
    return candidates.map((candidate: any) => (
      <CandidateCard key={candidate.id} candidate={candidate} onDoubleClicked={() => onDoubleClickedCandidate(candidate)} />
    ));
  }

  return (
    <>
    {selectedCandidate && (
      <EditCandidateModal
        candidate={selectedCandidate}
        open={openEditModal}
        setOpen={setOpenEditModal}
        key={selectedCandidate.id}
        refreshCandidates={fetchCandidates}
        refreshSkills={fetchSkills}
        skills={skills}
      />
    )}
      <h1>HR Platform</h1>
      <br />
      <hr className='mb-4 border-2 rounded-sm'/>
      <div className='container'>
        <h3>There's a new candidate? Add them here:</h3>
        <Button className="bg-[var(--text-name)]" onClick={() => setOpenAddModal(true)}>
          Add candidate
        </Button>
        <AddCandidateModal 
          open={openAddModal}
          setOpen={setOpenAddModal}
          refreshCandidates={fetchCandidates}
          refreshSkills={fetchSkills}
          skills={skills}/>
      </div>
      <hr className='mt-4 mb-0 border-2 rounded-sm'/>
      <div className='container'>
        <div className='options'>
          <input
            className='search-input'
            type="text"
            placeholder="Search candidates by name..."
            value={query}
            onChange={(e) => setQuery(e.target.value)}
          />

          <SkillsFilter skills={skills} selectedSkills={selectedSkills} 
            onSkillSelect={setSelectedSkills} refreshSkills={fetchSkills}/>
        </div>
      </div>
      <div className="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
        {renderCandidates()}
      </div>
    </>
  )
}

export default App
