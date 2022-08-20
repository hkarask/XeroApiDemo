import { Box, Heading, Text } from "@chakra-ui/react";
import { CheckCircleIcon, WarningTwoIcon } from "@chakra-ui/icons";

type FeedbackVariant = "warning" | "success";

type FeedbackProps = {
  heading: string;
  variant: FeedbackVariant;
  message: string;
};

const icons: { [key in FeedbackVariant]: JSX.Element } = {
  success: <CheckCircleIcon boxSize={'50px'} color={'green.500'} />,
  warning: <WarningTwoIcon boxSize={'50px'} color={'orange.300'} />
};

const Feedback = ({ variant, heading, message }: FeedbackProps) => {
  const icon = icons[variant];

  return (
    <Box textAlign="center" py={10} px={6}>
      {icon}
      <Heading as="h2" size="lg" mt={6} mb={2}>
        {heading}
      </Heading>
      <Text color={"gray.500"}>{message}</Text>
    </Box>
  );
};

export default Feedback;
