export interface ExaminationDto {
  examId: number;
  examName: string;
  examCategory: string
 }

 export interface Examination {
  examId: number;
  examName: string;
  examCategory: string
 }

 export interface InputCreateExamination {
  examName: string;
  examCategory: string
 }

 export interface ExaminationAutocompleteDto {
  examId: number;
  examName: string;
}
