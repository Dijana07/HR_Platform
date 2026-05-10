import { useEffect, useState } from 'react'
import './App.css'
import CandidateCard from './components/candidates/CandidateCard'
import { getCandidates, searchCandidates } from './api/candidateApi'
import { getSkills } from './api/skillApi'
import AddCandidateModal from './components/candidates/AddCandidateModal'
import SkillsFilter from './components/skills/SkillsFilter'
import EditCandidateModal from './components/candidates/EditCandidateModal'
import type { SkillDTO } from './domain/SkillDTO'
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

  useEffect(() => {
    fetchCandidates();

    const fetchSkills = async () => {
      const response = await getSkills();
      setSkills(response || []);
    }
    
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
      />
    )}
      <h1>HR Platform</h1>
      <br />
      <hr className='mb-4 border-2 rounded-sm'/>
      <div className='container'>
        <h3>There's a new candidate? Add them here:</h3>
        <Button onClick={() => setOpenAddModal(true)}>
          Add Candidate
        </Button>
        <AddCandidateModal 
          open={openAddModal}
          setOpen={setOpenAddModal}
          refreshCandidates={fetchCandidates}/>
      </div>
      <hr className='mt-4 mb-0 border-2 rounded-sm'/>
      <div className='container'>
        <div className='options'>
          <input
            type="text"
            placeholder="Search candidates..."
            value={query}
            onChange={(e) => setQuery(e.target.value)}
          />

          <SkillsFilter skills={skills} selectedSkills={selectedSkills} 
            onSkillSelect={setSelectedSkills} />
        </div>
      </div>
      <div className="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
        {renderCandidates()}
      </div>
    </>
  )
}

export default App
