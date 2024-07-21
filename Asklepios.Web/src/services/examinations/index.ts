import { type PaginationParams } from '@/models/paginationParams';
import httpClient from '../httpClient';
import { type Examination, type ExaminationDto, type InputCreateExamination } from '@/models/Examinations/examination';

const base = 'examinations-module/Examinations';

async function getPaginatedExaminations(pagination: PaginationParams) {
  return await httpClient.get<Examination[]>(base, { params: pagination });
}

async function deleteExamination(examId: number) {
  return await httpClient.delete<boolean>(`${base}/${examId}`);
}

async function createExamination(input: InputCreateExamination) {
  return await httpClient.post<ExaminationDto>(base, input);
}

async function getExaminationInfo(examId: number) {
  return await httpClient.get<ExaminationDto>(`${base}/${examId}`);
}

async function updateExamination(examId: number, input: ExaminationDto) {
  return await httpClient.put<void>(`${base}/${examId}`, input, {
    headers: {
      'Content-Type': 'application/json',
    },
  });
}

async function getExaminationsByCategory(category: string, pagination: PaginationParams) {
  return await httpClient.get<Examination[]>(`${base}/examinationsByCategory/${category}`, { params: pagination });
}

export default {
  getPaginatedExaminations,
  deleteExamination,
  createExamination,
  getExaminationInfo,
  updateExamination,
  getExaminationsByCategory
};
