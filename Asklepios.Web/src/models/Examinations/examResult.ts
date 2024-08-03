export interface ExamResultDto {
  examResultId: string;
  patientId: string;
  date: Date;
  result: string;
  comment: string;
  examId: number;
 }

 export interface ExamResultListDto {
  examResultId: string;
  patientId: string;
  date: Date;
  result: string;
  comment: string;
  examName: string;
 }

 export class InputCreateExamResult {
  examResultId  = '';
  patientId = '';
  date = '';
  result = '';
  comment = '';
  examId = '';
 }
