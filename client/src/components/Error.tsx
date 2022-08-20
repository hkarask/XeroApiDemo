import {
  Alert,
  AlertDescription,
  AlertIcon,
  AlertTitle,
} from "@chakra-ui/react";

const Error = ({
  title,
  description,
}: {
  title?: string;
  description?: string;
}) => {
  if (!description) return null;
  return (
    <Alert status="error">
      <AlertIcon />
      {title && <AlertTitle>{title}</AlertTitle>}
      {description && <AlertDescription>{description}</AlertDescription>}
    </Alert>
  );
};

export default Error;
